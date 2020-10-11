using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Warlocked.Entities.Units;

using WarnerEngine.Lib.Helpers;
using WarnerEngine.Services;

namespace Warlocked.Engine.Services
{
    public class UnitControlService : IService
    {
        private const string RENDER_TARGET_KEY = "UCS_render_target";
        public int AssignedTeam { get; private set; }

        public HashSet<IUnit> SelectedUnits { get; private set; }

        public HashSet<Type> GetDependencies()
        {
            return new HashSet<Type>() { typeof(IInputService) };
        }

        public void Initialize() 
        {
            SelectedUnits = new HashSet<IUnit>();
            GameService.GetService<IEventService>().Subscribe(
                Events.INTERNAL_RESOLUTION_CHANGED,
                _ =>
                {
                    IRenderService renderService = GameService.GetService<IRenderService>();
                    renderService.AddRenderTarget(
                        RENDER_TARGET_KEY,
                        renderService.InternalResolutionX,
                        renderService.InternalResolutionY,
                        RenderTargetUsage.PreserveContents
                    );
                }
            );
        }

        public void PreDraw(float DT)
        {
            IInputService inputService = GameService.GetService<IInputService>();
            Vector2 mousePosition = inputService.GetMouseInWorldSpace2();

            IUnit hoveredUnit = null;
            List<IUnit> units = GetUnitsFromCurrentScene();
            foreach (IUnit unit in units)
            {
                if (unit.SelectableArea.Contains(mousePosition))
                {
                    hoveredUnit = unit;
                }
            }

            if (inputService.WasLeftMouseButtonClicked())
            {
                if (hoveredUnit != null && hoveredUnit.Team == AssignedTeam)
                {
                    if (!inputService.IsLeftShiftHeld())
                    {
                        UnSelectAllUnits();
                    }
                    SelectedUnits.Add(hoveredUnit);
                    hoveredUnit.Select();
                }
                else
                {
                    UnSelectAllUnits();
                }
            }

            if (inputService.WasRightMouseButtonClicked())
            {
                foreach (IUnit unit in SelectedUnits)
                {
                    // Allow queuing move points when left shift is held
                    if (hoveredUnit == null)
                    {
                        unit.DirectToPoint(mousePosition, inputService.IsLeftShiftHeld());
                    }
                    else
                    {
                        unit.DirectToUnit(hoveredUnit);
                    }
                }
            }
        }

        public ServiceCompositionMetadata Draw()
        {
            GameService.GetService<IRenderService>()
                .SetRenderTarget(RENDER_TARGET_KEY, ClearColor: Color.Transparent)
                .Start(GameService.GetService<ISceneService>().CurrentScene.Camera.GetCenterPoint())
                .Render(DrawImplementation)
                .End()
                .Start()
                .FlushDeferredCalls()
                .End()
                .Cleanup();

            return new ServiceCompositionMetadata()
            {
                RenderTargetKey = RENDER_TARGET_KEY,
                Priority = 1,
                Tint = Color.White
            };
        }

        private void DrawImplementation()
        {
            IRenderService renderService = GameService.GetService<IRenderService>();

            IInputService inputService = GameService.GetService<IInputService>();
            Vector2 mousePosition = inputService.GetMouseInScreenSpace();

            renderService.AddDeferredCall(_ => GraphicsHelper.DrawSquare(
                new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 2, 2),
                Color.White,
                true)
            );

            foreach (IUnit unit in SelectedUnits)
            {
                GraphicsHelper.DrawSquare(unit.SelectableArea, Color.White);
            }
        }

        public void PostDraw() { }

        public Type GetBackingInterfaceType()
        {
            return typeof(UnitControlService);
        }

        public UnitControlService SetAssignedTeam(int AssignedTeam)
        {
            this.AssignedTeam = AssignedTeam;
            return this;
        }

        private List<IUnit> GetUnitsFromCurrentScene()
        {
            return GameService.GetService<ISceneService>().CurrentScene.GetEntitiesOfType<IUnit>();
        }

        private void UnSelectAllUnits()
        {
            foreach (IUnit unit in SelectedUnits)
            {
                unit.UnSelect();
            }
            SelectedUnits.Clear();
        }
    }
}

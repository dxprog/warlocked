using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using WarnerEngine.Lib;
using WarnerEngine.Services;
using WarnerEngine.Services.Implementations;

namespace Warlocked.Scenes
{
    public class PlayField : Scene
    {
        public const string SceneKey = "play_field";

        public override void OnSceneStart() 
        {
            Camera = new Camera(Vector2.Zero);
            AddEntity(Camera);
            AddEntity(new Entities.Units.Grunt(0, Vector2.Zero));
            AddEntity(new Entities.Units.Grunt(0, new Vector2(64, 0)));
            AddEntity(new Entities.Units.Grunt(1, new Vector2(64, 64)));
        }

        public override void OnSceneEnd() 
        {
            entities.Clear();
        }

        public override void Draw()
        {
            GameService.GetService<IRenderService>()
                .SetRenderTarget(SceneService.RenderTargets.CompositeTertiary, Color.Black)
                .Start(Camera.GetCenterPoint())
                .Render(base.Draw)
                .End()
                .Cleanup();
        }

        public override Dictionary<string, Func<string[], string>> GetLocalTerminalCommands()
        {
            return new Dictionary<string, Func<string[], string>>();
        }
    }
}

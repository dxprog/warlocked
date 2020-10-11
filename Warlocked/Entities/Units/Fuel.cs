using Microsoft.Xna.Framework;

using WarnerEngine.Lib.Components;
using WarnerEngine.Services;

namespace Warlocked.Entities.Units
{
    public class Fuel : AResource
    {
        private const int WIDTH = 32;
        private const int HEIGHT = 32;

        public override ResourceBundle.Resource ResourceType => ResourceBundle.Resource.Fuel;

        public override int MaxHealth => 10;

        public Fuel(Vector2 Position)
        {
            backingBox = new BackingBox(BackingBox.IType.Static, Position.X, 0, Position.Y, WIDTH, 0, HEIGHT);
        }

        public override void Draw()
        {
            GameService.GetService<IRenderService>().DrawQuad(
                GameService.GetService<IContentService>().GetWhiteTileTexture(),
                SelectableArea,
                new Rectangle(0, 0, 8, 8),
                Color.Green
            );
        }

        public override void PreDraw(float DT)
        {
            base.PreDraw(DT);
            if (remainingResources <= 0)
            {
                GameService.GetService<ISceneService>().CurrentScene.RemoveEntity(this);
            }
        }
    }
}

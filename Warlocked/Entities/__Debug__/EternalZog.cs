using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using WarnerEngine.Lib;
using WarnerEngine.Lib.Components;
using WarnerEngine.Lib.Entities;
using WarnerEngine.Services;

namespace Warlocked.Entities.__Debug__
{
    public sealed class EternalZog : ISceneEntity, IPreDraw, IDraw
    {
        private float rotation;

        public BackingBox GetBackingBox()
        {
            return BackingBox.Dummy;
        }

        public bool IsVisible()
        {
            return true;
        }

        public void OnAdd(Scene ParentScene) { }

        public void OnRemove(Scene ParentScene) { }

        public void PreDraw(float DT)
        {
            rotation += 2f * DT;
        }

        public void Draw()
        {
            Texture2D zogTexture = GameService.GetService<IContentService>().GetAsset<Texture2D>("zog_portrait");
            GameService.GetService<IRenderService>().DrawQuad(
                zogTexture, 
                Vector2.Zero, 
                new Rectangle(0, 0, zogTexture.Width, zogTexture.Height), 
                Origin: new Vector2(zogTexture.Width / 2, zogTexture.Height / 2),
                Rotation: rotation
            );
        }
    }
}

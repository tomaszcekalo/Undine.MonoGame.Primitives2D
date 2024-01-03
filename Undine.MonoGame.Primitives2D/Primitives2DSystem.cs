using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using Undine.Core;

namespace Undine.MonoGame.Primitives2D
{
    public class Primitives2DSystem : UnifiedSystem<Primitives2DComponent, TransformComponent>
    {
        private SpriteBatch SpriteBatch { get; set; }
        private Dictionary<Primitives2DDrawType, Action<Primitives2DComponent, TransformComponent>> _primitives2DDrawType;

        public Primitives2DSystem()
        {
            _primitives2DDrawType = new Dictionary<Primitives2DDrawType, Action<Primitives2DComponent, TransformComponent>>
            {
                { Primitives2DDrawType.DrawArc, DrawArc },
                { Primitives2DDrawType.DrawCircle, DrawCircle },
                { Primitives2DDrawType.DrawLine, DrawLine },
                { Primitives2DDrawType.DrawRectangle, DrawRectangle },
                { Primitives2DDrawType.FillRectangle, FillRectangle }
            };
        }

        private void FillRectangle(Primitives2DComponent arg1, TransformComponent arg2)
        {
            SpriteBatch.FillRectangle(arg2.Position - arg1.Size / 2, arg1.Size, arg1.Color, arg2.Rotation);
        }

        private void DrawRectangle(Primitives2DComponent arg1, TransformComponent arg2)
        {
            SpriteBatch.DrawRectangle(arg2.Position, arg1.Size, arg1.Color, arg1.Thickness);
        }

        private void DrawLine(Primitives2DComponent arg1, TransformComponent arg2)
        {
            SpriteBatch.DrawLine(arg2.Position, arg1.Size, arg1.Color, arg1.Thickness);
        }

        private void DrawCircle(Primitives2DComponent arg1, TransformComponent arg2)
        {
            SpriteBatch.DrawCircle(arg2.Position, arg1.Size.X, arg1.Sides, arg1.Color, arg1.Thickness);
        }

        private void DrawArc(Primitives2DComponent arg1, TransformComponent arg2)
        {
            SpriteBatch.DrawArc(arg2.Position, arg1.Size.X, arg1.Sides, arg2.Rotation, arg1.Size.Y, arg1.Color);
        }

        public override void ProcessSingleEntity(int entityId, ref Primitives2DComponent a, ref TransformComponent b)
        {
            _primitives2DDrawType[a.DrawType](a, b);
        }
    }
}
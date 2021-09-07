using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;
using System.Collections.Generic;
using Undine.Core;

namespace Undine.MonoGame.Primitives2D
{
    public class Primitives2DSystem : UnifiedSystem<Primitives2DComponent, TransformComponent>
    {
        private SpriteBatch _spriteBatch;
        private Dictionary<Primitives2DDrawType, Action<Primitives2DComponent, TransformComponent>> _primitives2DDrawType;

        public Primitives2DSystem(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _primitives2DDrawType = new Dictionary<Primitives2DDrawType, Action<Primitives2DComponent, TransformComponent>>();
            _primitives2DDrawType.Add(Primitives2DDrawType.DrawArc, DrawArc);
            _primitives2DDrawType.Add(Primitives2DDrawType.DrawCircle, DrawCircle);
            _primitives2DDrawType.Add(Primitives2DDrawType.DrawLine, DrawLine);
            _primitives2DDrawType.Add(Primitives2DDrawType.DrawRectangle, DrawRectangle);
            _primitives2DDrawType.Add(Primitives2DDrawType.FillRectangle, FillRectangle);
        }

        private void FillRectangle(Primitives2DComponent arg1, TransformComponent arg2)
        {
            _spriteBatch.FillRectangle(arg2.Position - arg1.Size / 2, arg1.Size, arg1.Color, arg2.Rotation);
        }

        private void DrawRectangle(Primitives2DComponent arg1, TransformComponent arg2)
        {
            _spriteBatch.DrawRectangle(arg2.Position, arg1.Size, arg1.Color, arg1.Thickness);
        }

        private void DrawLine(Primitives2DComponent arg1, TransformComponent arg2)
        {
            _spriteBatch.DrawLine(arg2.Position, arg1.Size, arg1.Color, arg1.Thickness);
        }

        private void DrawCircle(Primitives2DComponent arg1, TransformComponent arg2)
        {
            _spriteBatch.DrawCircle(arg2.Position, arg1.Size.X, arg1.Sides, arg1.Color, arg1.Thickness);
        }

        private void DrawArc(Primitives2DComponent arg1, TransformComponent arg2)
        {
            _spriteBatch.DrawArc(arg2.Position, arg1.Size.X, arg1.Sides, arg2.Rotation, arg1.Size.Y, arg1.Color);
        }

        public override void ProcessSingleEntity(int entityId, ref Primitives2DComponent a, ref TransformComponent b)
        {
            _primitives2DDrawType[a.DrawType](a, b);
        }
    }
}
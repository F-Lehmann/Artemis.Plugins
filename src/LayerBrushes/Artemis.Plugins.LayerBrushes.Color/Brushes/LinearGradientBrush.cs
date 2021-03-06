﻿using Artemis.Core.LayerBrushes;
using Artemis.Plugins.LayerBrushes.Color.PropertyGroups;
using SkiaSharp;

namespace Artemis.Plugins.LayerBrushes.Color
{
    public class LinearGradientBrush : LayerBrush<LinearGradientBrushProperties>
    {
        private float _scrollX;
        private float _scrollY;

        #region Overrides of LayerBrush<LinearGradientBrushProperties>

        /// <inheritdoc />
        public override void Render(SKCanvas canvas, SKRect bounds, SKPaint paint)
        {
            SKMatrix matrix = SKMatrix.Concat(
                SKMatrix.CreateTranslation(_scrollX, _scrollY),
                SKMatrix.CreateRotationDegrees(Properties.Rotation, bounds.MidX, bounds.MidY)
            );
            paint.Shader = SKShader.CreateLinearGradient(
                new SKPoint(bounds.Left, bounds.Top),
                new SKPoint(bounds.Right, bounds.Top),
                Properties.Colors.BaseValue.GetColorsArray(Properties.ColorsMultiplier),
                Properties.Colors.BaseValue.GetPositionsArray(Properties.ColorsMultiplier),
                SKShaderTileMode.Repeat,
                matrix
            );
            canvas.DrawRect(bounds, paint);
            paint.Shader?.Dispose();
            paint.Shader = null;
        }

        #endregion

        #region Overrides of BaseLayerBrush

        public override void EnableLayerBrush()
        {
        }

        public override void DisableLayerBrush()
        {
        }

        public override void Update(double deltaTime)
        {
            _scrollX += Properties.ScrollSpeed.CurrentValue.X * 10 * (float) deltaTime;
            _scrollY += Properties.ScrollSpeed.CurrentValue.Y * 10 * (float) deltaTime;
        }

        #endregion
    }
}
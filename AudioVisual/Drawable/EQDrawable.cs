using AudioVisual.Services;

namespace AudioVisual
{
    public class EQDrawable : IDrawable
    {
        public double[] values = new double[64];

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.White;

            float height = dirtyRect.Height;
            var barWidth = (dirtyRect.Width / values.Length);
            canvas.StrokeSize = barWidth - 1;
            var x = 0;
            foreach (var item in values)
            {
                var color = ColorHelper.GetFadedColor(Config.LevelColors[0], Config.LevelColors[1], item);

                canvas.StrokeColor = color;

                var lineStartX = ((x + 1) * barWidth) + (barWidth / 2);
                var lineStartY = height;
                var lineValueX = (x + 1) * barWidth + (barWidth / 2);
                var lineValueY = height - (height * (float)values[x]);
                canvas.DrawLine(lineStartX, lineStartY, lineValueX, lineValueY);

                Frequency freq = Config.FreqConfig.Frequencies[x];

                canvas.DrawString(x.ToString(), lineStartX, lineStartY, HorizontalAlignment.Left);
                canvas.DrawString(item.ToString("#0.00"), lineStartX - barWidth / 2, lineStartY - 20, HorizontalAlignment.Left);
                canvas.DrawString((freq.FreqRange?[0] ?? freq.Freq).ToString(), lineStartX - barWidth / 2, lineStartY - 40, HorizontalAlignment.Left);
                x++;
            }
        }
    }
}

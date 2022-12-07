namespace AudioVisual
{
    public class EQDrawable : IDrawable
    {
        public double[] values = new double[64];

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.White;

            float height = dirtyRect.Height;
            var barWidth = (dirtyRect.Width / values.Length) - 2;
            canvas.StrokeSize = barWidth - 1;
            var x = 0;
            foreach (var item in values)
            {
                if (item < 0.2)
                {
                    canvas.StrokeColor = Colors.Green;
                }
                if (item > 0.2)
                {
                    canvas.StrokeColor = Colors.Lime;
                }
                if (item > 0.5)
                {
                    canvas.StrokeColor = Colors.Yellow;
                }
                if (item > 0.8)
                {
                    canvas.StrokeColor = Colors.Red;
                }

                canvas.DrawLine(((x + 1) * barWidth), height, (x + 1) * barWidth, height - (height * (float)values[x]));
                x++;
            }
        }
    }
}

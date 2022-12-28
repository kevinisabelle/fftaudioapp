namespace AudioVisual
{
    public class GraphDrawable : IDrawable
    {
        public double[] values = new double[512];

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 2;
            float height = dirtyRect.Height;
            var x = 0;
            foreach (var item in values)
            {
                var yposition = height - ((float)values[x] + (200));
                canvas.DrawLine((x) * 2, height, (x * 2), yposition - 1);
                x++;
            }
        }
    }
}

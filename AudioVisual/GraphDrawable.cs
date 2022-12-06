namespace AudioVisual
{
    public class GraphDrawable : IDrawable
    {
        public double[] values = new double[512];

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            /*LinearGradientPaint linearGradientPaint = new LinearGradientPaint
            {
                StartColor = Colors.Yellow,
                EndColor = Colors.Green,
                // StartPoint is already (0,0)
                EndPoint = new Point(1, 0)
            };*/

            //RectF linearRectangle = new RectF(0, 0, 2, ((int)values[10]));
            //canvas.SetFillPaint(linearGradientPaint, linearRectangle);
            //canvas.SetShadow(new SizeF(10, 10), 10, Colors.Grey);
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 1;
            float height = dirtyRect.Height;
            var x = 0;
            foreach (var item in values)
            {
                canvas.DrawLine(x - 1, 0, x, (height * (float)values[x]));
                x++;
            }

        }


    }
}

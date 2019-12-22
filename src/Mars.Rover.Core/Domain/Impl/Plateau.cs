namespace Mars.Rover.Core.Domain.Impl
{
	public class Plateau : Rectangular, IPlateau
	{
		public int Bottom => 0;
        public int Left => 0;
        public int Right => Width;
        public int Top => Height;

        private object[,] _grid;
		public object[,] Grid => _grid;

        public Plateau (int width, int height)
		{
			Width = width;
			Height = height;

			_grid = new object[Right, Top];

			for (int i = Left; i < Right; i++) {
				for (int j = Bottom; j < Top; j++) {
					_grid [i, j] = $"{i}x{j}";
				}
			}			
		}
	}
}
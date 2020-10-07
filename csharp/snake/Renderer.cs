using System;

namespace beer_n_coding
{
    public class ConsoleRenderer 
    {
        private int m_height;
        private int m_width;

        public ConsoleRenderer(int height, int width)
        {
            m_height = height;
            m_width = width;
        }

        public void DrawHead(Vector v, ConsoleKey direction)
        {
            if (direction == ConsoleKey.UpArrow)
                Draw('^', v);
            else if (direction == ConsoleKey.DownArrow)
                Draw('v', v);
            else if (direction == ConsoleKey.LeftArrow)
                Draw('<', v);
            else if (direction == ConsoleKey.RightArrow)
                Draw('>', v);
        }

        public void DrawBody(Vector v)
        {
            Draw('x', v);
        }

        public void DrawApple(Vector v)
        {
            Draw('O', v);
        }

        public void Clear(Vector v)
        {
            Draw(' ', v);
        }

        public void RenderGameOver(){
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game over: start again? (enter for continue, esc for quit)");
        }

        public void Init(GameState state)
        {
            Console.Clear();

            for (int x = 0; x < m_height; x++)
            {
                for (int y = 0; y < m_width; y++)
                {
                    Console.Write(' ');                    
                }

                Console.Write(Environment.NewLine);
            }

            DrawApple(state.Apple);

            var head = state.Snake[0];

            DrawHead(head, state.Direction);

            foreach (var v in state.Snake)
            {
                DrawBody(v);
            }
        }

        public void DrawDebugInfo(GameState state, long ms){
            var head = state.Snake[0];

            Console.SetCursorPosition(0, m_height+1);
            Console.Write($"head:[{head.X}:{head.Y}] apple:[{state.Apple.X}:{state.Apple.Y}] direction:{state.Direction} score:{state.Snake.Count} rendertime:{ms}ms");
        }

        private void Draw(char c, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        private void Draw(char c, Vector v)
        {
            Draw(c, v.X, v.Y);
        }
    }
}
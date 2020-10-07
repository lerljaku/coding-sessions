using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace beer_n_coding
{
    public struct Vector{
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
        public bool Equals(Vector vector){
            return vector.X == X && vector.Y == Y;
        }
    }

    public class GameState
    {
        private int m_height;
        private int m_width;
        private ConsoleRenderer m_renderer;

        Random m_random = new Random();

        public GameState(int width, int height, ConsoleRenderer renderer)
        {
            m_width = width;
            m_height = height;

            m_renderer = renderer;

            Apple = new Vector(3,0);
            Snake.Add(new Vector(0,0));
            Direction = ConsoleKey.RightArrow;
        }

        public List<Vector> Snake {get;} = new List<Vector>();
        public Vector Apple { get; set; } 
        public ConsoleKey Direction { get; set; }

        public bool IsGameOver()
        {
            var head = Snake.First();

            return Snake.Count > 1 && Snake.Skip(1).Any(v => head.Equals(v));
        }

        public void MoveSnake()
        {
            var head = Snake.First();
            var newHead = new Vector(head.X, head.Y);
        
            if (Direction == ConsoleKey.UpArrow)
                newHead.Y = newHead.Y - 1;
            else if (Direction == ConsoleKey.DownArrow)
                newHead.Y = newHead.Y + 1;
            else if (Direction == ConsoleKey.LeftArrow)
                newHead.X = newHead.X - 1;
            else if (Direction == ConsoleKey.RightArrow)
                newHead.X = newHead.X + 1;
            else 
                throw new Exception($"Key {Direction} not supported.");
            
            Snake.Insert(0, newHead);
            m_renderer.DrawHead(newHead, Direction);

            if (newHead.Equals(Apple))
            {    
                var freeSpaces = new List<Vector>();
            
                for (int x = 0; x < m_width; x++)
                {
                    for (int y = 0; y < m_height; y++)
                    {
                        freeSpaces.Add(new Vector(x, y));   
                    }
                }

                var randomIndex = m_random.Next(freeSpaces.Count);
                var randomVector = freeSpaces.ElementAt(randomIndex);
                
                Apple = randomVector;
                m_renderer.DrawApple(Apple);
            }
            else
            {
                var tail = Snake.Last();
                Snake.RemoveAt(Snake.Count - 1);
                m_renderer.Clear(tail);
            }
        }
    }

    class Game{
        private GameState m_state;
        private ConsoleRenderer m_renderer;

        public Game(GameState state, ConsoleRenderer renderer)
        {
            m_state = state;
            m_renderer = renderer;
        }
        
        public void Run()
        {
            StartDispatcher();
            StartEventQueue();
        }

        private void StartDispatcher()
        {
            m_renderer.Init(m_state);

            Task.Run(() => 
            {
                var sw = new Stopwatch();
                var refreshIntervalMs = 100;

                while(true)
                {
                    sw.Restart();

                    m_state.MoveSnake();

                    m_renderer.DrawDebugInfo(m_state, sw.ElapsedMilliseconds);

                    Thread.Sleep(TimeSpan.FromMilliseconds(refreshIntervalMs - sw.ElapsedMilliseconds));

                    if (m_state.IsGameOver())
                    {
                        m_renderer.RenderGameOver();
                        break;
                    }
                }   
            });     
        }
        
        private void StartEventQueue()
        {
            while(true)
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow || 
                    key == ConsoleKey.DownArrow ||  
                    key == ConsoleKey.LeftArrow || 
                    key == ConsoleKey.RightArrow)
                    {
                        m_state.Direction = key;
                        m_renderer.DrawHead(m_state.Snake[0], key);
                    }
            }   
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var width = Console.WindowWidth-1;
            var height = Console.WindowHeight-1;

            var renderer = new ConsoleRenderer(width, height);
            var state = new GameState(width, height, renderer);

            var game = new Game(state, renderer);

            game.Run();

            Console.WriteLine($"snake length: {state.Snake.Count}.");
        }
    }
}

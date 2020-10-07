import threading
import time
import os
import sys


class Point:
    def __init__(self, x, y):
        self.x = x
        self.y = y


def clear_console():
    os.system('cls' if os.name == 'nt' else 'clear')


def print_at(x: int, y: int, text: str):
    sys.stdout.write("\x1b7\x1b[%d;%df%s\x1b8" % (x, y, text))
    sys.stdout.flush()


def print_at(p: Point, text: str):
    print(p.x, p.y, text)


def print_head(p: Point, direction: str):
    if direction == 'w':
        print_at(p, '^')
    if direction == 's':
        print_at(p, 'v')
    if direction == 'a':
        print_at(p, '<')
    if direction == 'd':
        print_at(p, '>')


class GameState:
    def __init__(self, columns: int, lines: int):
        self.direction = 'd'
        self.snake = [Point(0, 0)]
        self.apple = Point(2, 0)
        self.columns = columns
        self.lines = lines

    def move_snake(self):
        current_head = self.snake[0]
        new_head = Point(current_head.x, current_head.y)

        if self.direction == 'w':
            new_head.y -= 1
        if self.direction == 's':
            new_head.y += 1
        if self.direction == 'a':
            new_head.x -= 1
        if self.direction == 'd':
            new_head.x += 1

        self.snake.insert(0, new_head)
        print_head(new_head, self.direction)

        if new_head.x == self.apple.x and new_head.y == self.apple.y:
            tail = self.snake[-1]
            self.snake.remove(tail)
            print_at(tail, ' ')
        #todo generatte new apple


        return

    def is_game_over(self):
        head = self.snake[0]

        if head.x < 0 or head.x > self.columns or head.y < 0 or head.y > self.lines:
            return True

        body = iter(self.snake)
        next(body)

        for p in body:
            if p.x == head.x and p.y == head.y:
                return True

        return False


def process_input(state: GameState):
    while True:
        key = input()
        if key == 'w' or key == 's' or key == 'a' or key == 'd':
            state.direction = key


def start_game(state: GameState):
    refresh_interval_s = 0.2
    t = threading.Thread(target=process_input, args=(1,))
    t.start()
    clear_console()

    for line in range(state.lines):
        for col in range(state.columns):
            print(' ')
        print('\n')

    while True:
        state.move_snake()

        if state.is_game_over():
            break

        time.sleep(refresh_interval_s)


if __name__ == '__main__':
    size = os.get_terminal_size()
    start_game(GameState(size.columns, size.lines))

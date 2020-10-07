import hashlib


def square(a: int):
    return a * a


col = [5,43,5,346,456,45,6]

jmena = [ "anicka", "alena", "bara", "zuzana"]

col2 = list(map(square, col))

for mocnina in col2:
    print(mocnina)

map(lambda number: "0" if number < 5 else "1", col)

for number in col:
    x = "0" if number < 5 else "1"

if "5" < "6":
    c = "0"
else:
    c = "1"

c = "0" if "5" < "6" else "1"

''.join()


col2 = list(map(lambda a: a*a, col))
sudy = list(filter(lambda a: a % 2 == 0, col))

sudy = list(filter(lambda jmeno: jmeno.startswith("a"), jmena))

print(sudy)

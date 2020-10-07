import sys

path: str = "characters.txt"
delimeter: str = ";"


class Character:
    def __init__(self, cls: str, name: str, race: str, strength: int, intellect: int, charisma: int, agility: int, wisdom: int):
        self.strength = strength
        self.cls = cls
        self.name = name
        self.race = race
        self.intellect = intellect
        self.charisma = charisma
        self.agility = agility
        self.wisdom = wisdom

    def __str__(self):
        """ kdyz zavolam str(Character) zavola se toto """
        set = (self.cls, self.name, self.race, self.strength, self.intellect, self.charisma, self.agility, self.wisdom)
        result = ""
        for attribute in set:
            result += str(attribute) + delimeter
        return result


def load_character() -> Character:
    with open(path, "r") as file:
        char_str = file.read()
        splited = char_str.split(delimeter)
        return Character(splited[0], splited[1], splited[2], int(splited[3]), int(splited[4]), int(splited[5]), int(splited[6]), int(splited[7]))


def save_character(character: Character):
    with open(path, "w") as file:
        file.write(str(character))


if sys.argv[1] == "-l":
    print(load_character())
else:
    character = Character(cls=sys.argv[1], name=sys.argv[2], race=sys.argv[3],  strength=int(sys.argv[4]), intellect=int(sys.argv[5]), charisma=1, agility=1, wisdom=20)

    save_character(character)


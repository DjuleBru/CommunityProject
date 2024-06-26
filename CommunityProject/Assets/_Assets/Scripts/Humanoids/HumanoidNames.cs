using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidNames
{
    public static List<string> HumanNames = new List<string> {
        "Bobo Bumble",
        "Jumbolia George",
        "Kevin",
        "Glandalf le gland",
        "Scotti",
        "Bebs",
        "Chuck Norris",
        "Albert",
        "Bobbus",
        "Bob",
        "Rob Anybody",
        "Hans Mustermann",
        "Fredrick",
        "Robert de la Batte",
        "Diego",
        "Om",
        "UnPaws",
        "Harold",
        "Brian",
        "Joe Biden",
        "Sarah",
        "Rebecca",
        "Wanona",
        "Derrick",
        "Mads",
        "Augustus Octagon",
        "Claudius Limp",
        "Marcus Sternface",
        "Lucius Sternface",
        "Otto Sachsenfowler",
        "Henry Carpet",
        "Fredrick Potsdam",
        "Rudolf Arthome",
        "Leopold Lacroix",
        "Louis Younglion",
        "Philip Fairbold",
        "Charles Winsmore",
        "Lucas",
        "Sarah Connor",
        "Stupid Dude",
        "Derud",
        "Chad",
        "Augustus hominem",
        "hulfman stillwagon",
        "emanuel",
        "Wobbelmann",
        "Joniscleiton",
    };

    public static List<string> ElfNames = new List<string> {
        "Jean-pedrovitch",
        "Yonndervitch",
        "Elves Presley",
        "Logelas",
        "Benjamin Button",
        "Angelo Smallwing",
        "Loyfer Drasil",
        "Immeral",
        "F�anor",
        "Mori",
        "Legolas",
        "Melf",
        "Bronwynafred",
        "drizzt do'urden",
        "Limlast the magnificient",
        "Lamolist the brave",
        "Lerolim the most humble",
        "Likola the undefeated",
        "Lindola the longear",
        "Lemola the mighty",
        "Lamolas the original",
        "Lindor the wonka",
        "Lytalist the treehugger",
        "Lerlast the lazy",
        "Lastlim the aboveaverage",
        "Listolam the null",
        "Hallo",
        "santus francescus",
        "snorri",
        "Elas",
        "Hyphen",
        "Carisvaldo",
    };

    public static List<string> OrcNames = new List<string> {
        "Thraka",
        "Smack Smash",
        "Brick",
        "Baltasar za best",
        "Hugagug",
        "Aaaargh",
        "Kaighenrun",
        "Gazgull",
        "Grunk the Grumbler",
        "Gorgy",
        "Kroum",
        "Grog",
        "Grak",
        "Yez",
        "Truk",
        "orgcra",
        "Gronk",
        "Scarlet Pimperne",
        "Toad Lily",
        "Pincushion Flower",
        "Bear's Breeches",
        "Sneezewort",
        "Malte",
        "Tod Pod",
        "Chop Job",
        "Bork",
        "Todd Howard",
        "Warts",
        "grog",
        "strobe",
        "ohyzd",
        "Gnaw",
        "troble wit an clubd",
        "Kass La Krass",
        "Glimbo",
        "Olga",
    };

    public static List<string> DwarvesNames = new List<string> {
        "Pierre",
        "Chip Chop",
        "Steve",
        "Halfdan",
        "Tom holland",
        "Finn",
        "Krobim Hornshield",
        "Dig Hole",
        "Meredith Gray",
        "Fradith Axe-Eater",
        "Pick Haxe",
        "Gimli",
        "Thorin Oakenshield",
        "Joe",
        "Aza",
        "Heirluum",
        "Max",
        "Urist Twelvefingers",
        "Bomrek Boatmurder",
        "Doren Diamondwork",
        "Nil Datanull",
        "Erib Rockstone",
        "Kogsak Elfleather",
        "Ral Furnacegold",
        "Thob Fishcleaner",
        "Mebzuth Soapsmith",
        "Litast Readgood",
        "Cilob Beardless",
        "Dodok Goblinmeat",
        "Aurumna",
        "Stegfeld",
        "Andvari",
        "Topiar",
        "L'loreon",
        "Jean-Pierre",
        "bernis chicus",
        "bjorn strong in the arm",
        "Groomoug Woldbelt",
        "Norman",
    };

    public static List<string> HalfingsNames = new List<string> {
        "Bilbo Baggins",
        "Frodo",
        "Brodo Feutlin",
        "Bob McFooter",
        "Fred",
        "Wilfried Juggleball",
        "Dru Hum Yard",
        "Milli Hearthwarm",
        "Widdle",
        "Halfy",
        "Shleem",
        "Areida",
        "Bodil",
        "Proingo",
        "noxi bornaked",
        "doxi lulu",
        "lulu darling",
        "steven knox",
        "darwin twirks",
        "ben",
        "Tom Bombadil",
        "Unterberg",
        "Pat",
        "Bob",
        "Stan",
        "Gerry",
        "Barb",
        "Mike",
        "Frank",
        "Kathy",
        "Doug",
    };

    public static List<string> GoblinsNames = new List<string> {
        "Schlumpfi",
        "Griz",
        "schlep",
        "Cromp",
        "Trags",
        "Olivia Benson",
        "Shrubby",
        "Molt Droker",
        "Tinny",
        "Gubert",
        "Barack",
        "Snuggle Snot",
        "Marius",
        "Gatto",
        "Blorgnak",
        "Squiby",
        "Chomp Homp",
        "blarg",
        "hilfly",
        "gibles",
        "dornobs",
        "pony",
        "gaggle",
        "grok",
        "grock",
        "flocker",
        "nackle",
        "spiner",
        "stopper",
        "flitter",
        "flogger",
        "Brick",
    };

    public static string GetRandomName(HumanoidSO.HumanoidType humanoidType) {

        if(humanoidType == HumanoidSO.HumanoidType.Human) {
            return HumanNames[Random.Range(0, HumanNames.Count)];
        }

        if (humanoidType == HumanoidSO.HumanoidType.Elf) {
            return ElfNames[Random.Range(0, ElfNames.Count)];
        }

        if (humanoidType == HumanoidSO.HumanoidType.Orc) {
            return OrcNames[Random.Range(0, OrcNames.Count)];
        }

        if (humanoidType == HumanoidSO.HumanoidType.Dwarf) {
            return DwarvesNames[Random.Range(0, DwarvesNames.Count)];
        }

        if (humanoidType == HumanoidSO.HumanoidType.Halfling) {
            return HalfingsNames[Random.Range(0, HalfingsNames.Count)];
        }

        if (humanoidType == HumanoidSO.HumanoidType.Goblin) {
            return GoblinsNames[Random.Range(0, GoblinsNames.Count)];
        }

        return null;
    }
}

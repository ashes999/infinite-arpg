from Microsoft.Xna.Framework import Color, Vector2

from DeenGames.InfiniteArpg.Ecs import Entity
from DeenGames.InfiniteArpg.Ecs.Components import Drawable
from DeenGames.InfiniteArpg.Scenes import AbstractScene

class CoreGameScene(AbstractScene):

    WALL_THICKNESS = 16

    def __init__(self, graphicsDevice):
        super(CoreGameScene, self).__init__()
        self.ClearColour = Color.DarkGreen
        self.player = self.Add(Entity("player").Colour(Color.Red, 32, 32).Move(300, 200))
        self.player.MoveToArrowKeys(200)
        self.makeWalls()

    def makeWalls(self):
        self.Add(Entity("wall").Colour(Color.Gray, self.Width, CoreGameScene.WALL_THICKNESS).Move(0, 0))
        self.Add(Entity("wall").Colour(Color.Gray, self.Width, CoreGameScene.WALL_THICKNESS).Move(0, self.Height - CoreGameScene.WALL_THICKNESS))
        self.Add(Entity("wall").Colour(Color.Gray, CoreGameScene.WALL_THICKNESS, self.Height).Move(0, 0))
        self.Add(Entity("wall").Colour(Color.Gray, CoreGameScene.WALL_THICKNESS, self.Height).Move(self.Width - CoreGameScene.WALL_THICKNESS, 0))
        
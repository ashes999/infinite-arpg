from Microsoft.Xna.Framework import Color

from DeenGames.InfiniteArpg.Ecs import Entity
from DeenGames.InfiniteArpg.Scenes import AbstractScene

class CoreGameScene(AbstractScene):

    WALL_THICKNESS = 16

    def __init__(self, graphicsDevice):
        super(CoreGameScene, self).__init__()
        self.clearColour = Color.DarkGreen
        self.player = self.add(Entity("player").color(Color.Red, 32, 32).move(300, 200))
        self.player.move_to_arrow_keys(200)
        self.make_walls()

    def make_walls(self):
        self.add(Entity("wall").color(Color.Gray, self.width, CoreGameScene.WALL_THICKNESS).move(0, 0))
        self.add(Entity("wall").color(Color.Gray, self.width, CoreGameScene.WALL_THICKNESS).move(0, self.height - CoreGameScene.WALL_THICKNESS))
        self.add(Entity("wall").color(Color.Gray, CoreGameScene.WALL_THICKNESS, self.height).move(0, 0))
        self.add(Entity("wall").color(Color.Gray, CoreGameScene.WALL_THICKNESS, self.height).move(self.width - CoreGameScene.WALL_THICKNESS, 0))
        
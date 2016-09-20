from Microsoft.Xna.Framework import Color, Vector2

from DeenGames.InfiniteArpg.Ecs import Entity
from DeenGames.InfiniteArpg.Ecs.Components import Drawable
from DeenGames.InfiniteArpg.Scenes import AbstractScene

class CoreGameScene(AbstractScene):
    def __init__(self, graphicsDevice):
        super(CoreGameScene, self).__init__()
        self.ClearColour = Color.DarkGreen
        #self.player = Entity().Image('Content/trigger_bar_trigger.png')
        self.player = Entity().Colour(Color.Red, 32, 32).Move(300, 200)

    def Draw(self, spriteBatch):
        self.player.Draw(spriteBatch)


from Microsoft.Xna.Framework import Color, Vector2

from DeenGames.InfiniteArpg.Ecs.Components import Drawable
from DeenGames.InfiniteArpg.Scenes import AbstractScene

class CoreGameScene(AbstractScene):
    def __init__(self, graphicsDevice):
        super(CoreGameScene, self).__init__()
        self.ClearColour = Color.DarkGreen
        self.player = Drawable().Colour(self.Colour(Color.Red), 32, 32).Move(300, 200)

    def Draw(self, spriteBatch):
        self.player.Draw(spriteBatch)


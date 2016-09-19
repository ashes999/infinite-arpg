from Microsoft.Xna.Framework import Color, Rectangle, Vector2
from Microsoft.Xna.Framework.Graphics import SurfaceFormat, Texture2D

from DeenGames.InfiniteArpg.Scenes import AbstractScene

class Drawable():
    def __init__(self):
        self.isColour = None

    def image(self, imageTexture):
        self.isColour = False
        self.texture2D = imageTexture
        return self

    def colour(self, colourTexture, width, height):
        self.isColour = True
        self.texture2D = colourTexture
        self.width = width
        self.height = height
        return self

    def draw(self, spriteBatch):
        if (self.isColour == True):
            spriteBatch.Draw(self.texture2D, None, Rectangle(0, 0, self.width, self.height))
        else:
            spriteBatch.Draw(self.texture2D, Vector2.Zero)


class CoreGameScene(AbstractScene):
    def __init__(self, graphicsDevice):
        super(CoreGameScene, self).__init__()
        self.ClearColour = Color.DarkGreen
        self.player = Drawable().colour(self.Colour(Color.Red), 32, 32)

    def Draw(self, spriteBatch):
        self.player.draw(spriteBatch)


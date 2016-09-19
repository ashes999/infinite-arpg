from Microsoft.Xna.Framework import Color, Rectangle, Vector2
from Microsoft.Xna.Framework.Graphics import SurfaceFormat, Texture2D

from DeenGames.InfiniteArpg.Scenes import AbstractScene

class Drawable():
    def __init__(self, texture2D):
        self.texture2D = texture2D
        self.fillColour = None

    def colour(self, width, height):
        self.fillColour = True
        self.width = width
        self.height = height
        return self

    def draw(self, spriteBatch):
        if (self.fillColour != None):
            spriteBatch.Draw(self.texture2D, None, Rectangle(0, 0, self.width, self.height))
            #print("Hi there: f={0}, w={1}, h={2}".format(self.fillColour, self.width, self.height))
        else:
            spriteBatch.Draw(self.texture2D, Vector2.Zero)


class CoreGameScene(AbstractScene):
    def __init__(self, graphicsDevice):
        super(CoreGameScene, self).__init__()

        self.player = Drawable(self.LoadImage("Content/player.png"))
        #self.test = Drawable(self.Colour(Color.Red)).colour(100, 50)

    def Draw(self, spriteBatch):
        self.player.draw(spriteBatch)
        #self.test.draw(spriteBatch)


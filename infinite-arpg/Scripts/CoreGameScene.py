from Microsoft.Xna.Framework import Vector2

from DeenGames.InfiniteArpg.Scenes import AbstractScene

class CoreGameScene(AbstractScene):
    def __init__(self, graphicsDevice):
        super(CoreGameScene, self).__init__()
        self.player = self.LoadImage("Content/player.png")

    def Draw(self, spriteBatch):
        spriteBatch.Draw(self.player, Vector2.Zero)
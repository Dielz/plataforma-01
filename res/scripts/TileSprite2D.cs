using Godot;

namespace Plataforma01;

public partial class TileSprite2D : Sprite2D {
    [Export] public Vector2 CellSize = new Vector2(32, 32);

    public override void _Ready() {
        if (Texture == null || GetParent() is not StaticBody2D body) {
            return;
        }

        RectangleShape2D rect = null;
        CollisionShape2D hitShape = null;
        foreach (Node child in body.GetChildren()) {
            if (child is CollisionShape2D cs && cs.Shape is RectangleShape2D r && !cs.Disabled) {
                rect = r;
                hitShape = cs;
                break;
            }
        }
        if (rect == null) {
            return;
        }

        Image src = Texture.GetImage();
        if (src == null) {
            return;
        }

        int tilePixelW = src.GetWidth();
        int tilePixelH = src.GetHeight();
        float ppuX = tilePixelW / Mathf.Max(CellSize.X, 0.01f);
        float ppuY = tilePixelH / Mathf.Max(CellSize.Y, 0.01f);

        int imgPixelW = Mathf.Max(1, Mathf.CeilToInt(rect.Size.X * ppuX));
        int imgPixelH = Mathf.Max(1, Mathf.CeilToInt(rect.Size.Y * ppuY));

        var img = Image.CreateEmpty(imgPixelW, imgPixelH, false, Image.Format.Rgba8);
        var fullSrc = new Rect2I(0, 0, tilePixelW, tilePixelH);
        int cols = Mathf.CeilToInt(imgPixelW / (float)tilePixelW);
        int rows = Mathf.CeilToInt(imgPixelH / (float)tilePixelH);
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                img.BlitRect(src, fullSrc, new Vector2I(col * tilePixelW, row * tilePixelH));
            }
        }

        Position = hitShape.Position;
        Scale = new Vector2(1f / ppuX, 1f / ppuY);
        Texture = ImageTexture.CreateFromImage(img);
    }
}
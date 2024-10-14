using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ChatGPTButton : Button
{
    private Color _topGradientColor = Color.MediumSlateBlue;  // Light color for top (highlight)
    private Color _bottomGradientColor = Color.Gray;     // Darker color for bottom (shadow)
    private Color _hoverColor = Color.LightGray;         // Hover effect
    private Color _clickTopColor = Color.DarkGray;       // Click effect (darker top)
    private Color _clickBottomColor = Color.Black;       // Click effect (darker bottom)
    private Color _borderColor = Color.Black;            // Border color
    private int _borderRadius = 10;                      // Rounded corner radius

    private bool _isHovered = false;
    private bool _isClicked = false;

    public ChatGPTButton()
    {
        // Set some default properties
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.BackColor = Color.Transparent;
        this.ForeColor = Color.White;
        this.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Size = new Size(160, 60);  // Default button size
        this.Cursor = Cursors.Hand;
    }

    // Property for Border Radius
    public int BorderRadius
    {
        get { return _borderRadius; }
        set { _borderRadius = value; Invalidate(); }
    }

    // OnMouseEnter (Hover Effect)
    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        _isHovered = true;
        Invalidate();
    }

    // OnMouseLeave (Restore Default Background)
    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _isHovered = false;
        Invalidate();
    }

    // OnMouseDown (Click Effect)
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
        base.OnMouseDown(mevent);
        _isClicked = true;
        Invalidate();
    }

    // OnMouseUp (Restore Hover Effect After Click)
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        base.OnMouseUp(mevent);
        _isClicked = false;
        Invalidate();
    }

    // Paint the button with custom design
    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        Graphics g = pevent.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle buttonRect = this.ClientRectangle;
        buttonRect.Width -= 1;
        buttonRect.Height -= 1;

        // Rounded corners path
        GraphicsPath path = GetRoundedRectanglePath(buttonRect, _borderRadius);

        // Set gradient brush for 3D effect (normal, hover, or clicked)
        LinearGradientBrush brush;

        if (_isClicked)
        {
            // Darker 3D effect for click state
            brush = new LinearGradientBrush(buttonRect, _clickTopColor, _clickBottomColor, LinearGradientMode.Vertical);
        }
        else if (_isHovered)
        {
            // Lighter hover effect
            brush = new LinearGradientBrush(buttonRect, _hoverColor, _bottomGradientColor, LinearGradientMode.Vertical);
        }
        else
        {
            // Default 3D look with light on top and shadow on the bottom
            brush = new LinearGradientBrush(buttonRect, _topGradientColor, _bottomGradientColor, LinearGradientMode.Vertical);
        }

        // Fill button background with gradient
        g.FillPath(brush, path);

        // Draw border
        using (Pen borderPen = new Pen(_borderColor, 2))
        {
            g.DrawPath(borderPen, path);
        }

        // Add shadow effect (optional): Draw a slight shadow below the button for 3D depth
        Rectangle shadowRect = new Rectangle(buttonRect.X + 3, buttonRect.Y + 3, buttonRect.Width, buttonRect.Height);
        using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(60, Color.Black)))
        {
            g.FillPath(shadowBrush, GetRoundedRectanglePath(shadowRect, _borderRadius));
        }

        // Draw button text in the center
        TextRenderer.DrawText(
            g, this.Text, this.Font, this.ClientRectangle,
            this.ForeColor, Color.Transparent,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        );
    }

    // Utility method for rounded corners
    private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        float r = radius * 2f;

        path.AddArc(rect.X, rect.Y, r, r, 180, 90);  // Top-left corner
        path.AddArc(rect.X + rect.Width - r, rect.Y, r, r, 270, 90);  // Top-right corner
        path.AddArc(rect.X + rect.Width - r, rect.Y + rect.Height - r, r, r, 0, 90);  // Bottom-right corner
        path.AddArc(rect.X, rect.Y + rect.Height - r, r, r, 90, 90);  // Bottom-left corner
        path.CloseFigure();

        return path;
    }
}

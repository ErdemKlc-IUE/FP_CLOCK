using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_CLOCK
{
    public class CustomButton : Button
    {
        private int  borderSize = 2;
        private int borderRadius = 0;
        private Color borderColor = Color.White;
        private Color gradientStartColor = Color.WhiteSmoke;
        private Color gradientEndColor = Color.Silver;
        private Color hoverStartColor = Color.AliceBlue;
        private Color hoverEndColor = Color.LightBlue;

        


        private bool isHovered = false;

        [Category("Custom Props")]
        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; this.Invalidate(); }
        }

        [Category("Custom Props")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; this.Invalidate(); }
        }

        [Category("Custom Props")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        [Category("Custom Props")]
        public Color GradientStartColor
        {
            get { return gradientStartColor; }
            set { gradientStartColor = value; this.Invalidate(); }
        }

        [Category("Custom Props")]
        public Color GradientEndColor
        {
            get { return gradientEndColor; }
            set { gradientEndColor = value; this.Invalidate(); }
        }

        [Category("Custom Props")]
        public Color HoverStartColor
        {
            get { return hoverStartColor; }
            set { hoverStartColor = value; this.Invalidate(); }
        }

        [Category("Custom Props")]
        public Color HoverEndColor
        {
            get { return hoverEndColor; }
            set { hoverEndColor = value; this.Invalidate(); }
        }

        public CustomButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 50);
            this.BackColor = SystemColors.ScrollBar;
            this.ForeColor = SystemColors.ActiveCaptionText;
            this.Cursor = Cursors.Hand;
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Define the surface and border rectangles
            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 2, this.Height - 2);

            // Colors for gradient: Hover colors or default gradient colors
            Color startColor = isHovered ? hoverStartColor : gradientStartColor;
            Color endColor = isHovered ? hoverEndColor : gradientEndColor;

            if (borderRadius > 2)
            {
                // Rounded corners with gradient
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - 1))
                using (LinearGradientBrush brushSurface = new LinearGradientBrush(rectSurface, startColor, endColor, LinearGradientMode.Vertical))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(pathSurface);

                    // Fill the button background with gradient
                    e.Graphics.FillPath(brushSurface, pathSurface);

                    // Draw the border
                    if (borderSize >= 1)
                        e.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else
            {
                // Regular rectangle with gradient
                using (LinearGradientBrush brushSurface = new LinearGradientBrush(rectSurface, startColor, endColor, LinearGradientMode.Vertical))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(rectSurface);
                    e.Graphics.FillRectangle(brushSurface, rectSurface);

                    // Draw the border
                    if (borderSize >= 1)
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }

            // Draw the image (if there is an image)
            if (this.Image != null)
            {
                // Calculate the position for the image to be drawn (adjust as needed)
                int imageX = 75; // Slight padding from the left
                int imageY = (this.Height - this.Image.Height) / 2; // Vertically center the image

                // Draw the image on top of the gradient background
                e.Graphics.DrawImage(this.Image, imageX, imageY, this.Image.Width, this.Image.Height);
            }

            // Draw the text (centered next to the image)
            if (!string.IsNullOrEmpty(this.Text))
            {
                // Adjust the X position of the text to start after the image
                int textX = this.Image == null ? 0 : this.Image.Width + 20; // 20 is the padding after the image
                Rectangle textRect = new Rectangle(textX, 0, this.Width - textX, this.Height);

                TextRenderer.DrawText(
                    e.Graphics,
                    this.Text,
                    this.Font,
                    textRect,
                    this.ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter); // Align the text to the left and vertically center
            }
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            this.Invalidate(); // Redraw to apply hover effect
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            this.Invalidate(); // Redraw to revert hover effect
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (this.Parent != null)
            {
                this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
            }
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.Invalidate();
            }
        }

    }
}

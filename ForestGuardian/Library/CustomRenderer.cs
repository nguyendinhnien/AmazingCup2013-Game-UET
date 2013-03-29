using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
// For the NativeMethods helper class:
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Renderers;
using ProjectMercury.Emitters;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using ProjectMercury;

namespace Library
{
    public sealed class CustomRenderer : Renderer
    {
        public SpriteBatch Batch;

        private BlendState NonPremultipliedAdditive
        {
            get;set;
        }
        public CustomRenderer()
        {
        }

        protected override void Dispose(bool disposing)
        {
            bool batch = !disposing;
            if (!batch)
            {
                batch = this.Batch == null;
                if (!batch)
                {
                    this.Batch.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private BlendState GetBlendState(EmitterBlendMode emitterBlendMode)
        {
            BlendState nonPremultiplied;
            EmitterBlendMode emitterBlendMode1 = emitterBlendMode;
            switch (emitterBlendMode1)
            {
                case EmitterBlendMode.Alpha:
                    {
                        nonPremultiplied = BlendState.NonPremultiplied;
                        break;
                    }
                case EmitterBlendMode.Add:
                    {
                        nonPremultiplied = this.NonPremultipliedAdditive;
                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
            return nonPremultiplied;
        }

        public override void LoadContent(ContentManager content)
        {
            Guard.IsTrue(this.GraphicsDeviceService == null, "GraphicsDeviceService property has not been initialised with a valid value.");
            bool batch = this.Batch != null;
            if (!batch)
            {
                this.Batch = new SpriteBatch(this.GraphicsDeviceService.GraphicsDevice);
            }
            batch = this.NonPremultipliedAdditive != null;
            if (!batch)
            {
                BlendState blendState = new BlendState();
                blendState.AlphaBlendFunction = BlendFunction.Add;
                blendState.AlphaDestinationBlend = Blend.One;
                blendState.AlphaSourceBlend = Blend.SourceAlpha;
                blendState.ColorBlendFunction = BlendFunction.Add;
                blendState.ColorDestinationBlend = Blend.One;
                blendState.ColorSourceBlend = Blend.SourceAlpha;
                this.NonPremultipliedAdditive = blendState;
            }
        }

        public override void RenderEmitter(Emitter emitter, ref Matrix transform)
        {
            bool flag;
            Guard.ArgumentNull("emitter", emitter);
            Guard.IsTrue(this.Batch == null, "SpriteBatchRenderer is not ready! Did you forget to LoadContent?");
            flag = (emitter.ParticleTexture == null ? true : emitter.ActiveParticlesCount <= 0);
            bool blendMode = flag;
            if (!blendMode)
            {
                blendMode = emitter.BlendMode != EmitterBlendMode.None;
                if (blendMode)
                {
                    Rectangle rectangle = new Rectangle(0, 0, emitter.ParticleTexture.Width, emitter.ParticleTexture.Height);
                    Vector2 vector2 = new Vector2((float)rectangle.Width / 2f, (float)rectangle.Height / 2f);
                    BlendState blendState = this.GetBlendState(emitter.BlendMode);
                    this.Batch.Begin(SpriteSortMode.Deferred, blendState, null, null, null, null, transform);
                    int num = 0;
                    while (true)
                    {
                        blendMode = num < emitter.ActiveParticlesCount;
                        if (!blendMode)
                        {
                            break;
                        }
                        Particle particles = emitter.Particles[num];
                        float scale = particles.Scale / (float)emitter.ParticleTexture.Width;
                        this.Batch.Draw(emitter.ParticleTexture, particles.Position, new Rectangle?(rectangle), new Color(particles.Colour), particles.Rotation, vector2, scale, SpriteEffects.None, 0f);
                        num++;
                    }
                    this.Batch.End();
                }
            }
        }
    }

    internal static class Guard
    {
        public static void ArgumentGreaterThan<T>(string parameter, T argument, T threshold)
        where T : IComparable<T>
        {
            bool flag = argument.CompareTo(threshold) <= 0;
            if (flag)
            {
                return;
            }
            else
            {
                throw new ArgumentOutOfRangeException(parameter);
            }
        }

        public static void ArgumentLessThan<T>(string parameter, T argument, T threshold)
        where T : IComparable<T>
        {
            bool flag = argument.CompareTo(threshold) >= 0;
            if (flag)
            {
                return;
            }
            else
            {
                throw new ArgumentOutOfRangeException(parameter);
            }
        }

        public static void ArgumentNotFinite(string parameter, float argument)
        {
            bool flag;
            flag = (float.IsNaN(argument) || float.IsNegativeInfinity(argument) ? false : !float.IsPositiveInfinity(argument));
            bool flag1 = flag;
            if (flag1)
            {
                return;
            }
            else
            {
                throw new NotFiniteNumberException((double)argument);
            }
        }

        public static void ArgumentNull(string parameter, object argument)
        {
            bool flag = argument != null;
            if (flag)
            {
                return;
            }
            else
            {
                throw new ArgumentNullException(parameter);
            }
        }

        public static void ArgumentNullOrEmpty(string parameter, string argument)
        {
            bool flag = !string.IsNullOrEmpty(argument);
            if (flag)
            {
                return;
            }
            else
            {
                throw new ArgumentNullException(parameter);
            }
        }

        public static void ArgumentOutOfRange<T>(string parameter, T argument, T min, T max)
        where T : IComparable<T>
        {
            bool flag;
            flag = (argument.CompareTo(min) < 0 ? false : argument.CompareTo(max) <= 0);
            bool flag1 = flag;
            if (flag1)
            {
                return;
            }
            else
            {
                throw new ArgumentOutOfRangeException(parameter);
            }
        }

        public static void IsFalse(bool expression, string message)
        {
            bool flag = expression;
            if (flag)
            {
                return;
            }
            else
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void IsTrue(bool expression, string message)
        {
            bool flag = !expression;
            if (flag)
            {
                return;
            }
            else
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}

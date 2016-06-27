using System;
using Autofac;
using OneModel.Compression.Decompression;

namespace OneModel.Compression.Autofac
{
    public class DecompressionModule : Module
    {
        public string PathTo7Zip { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            if (string.IsNullOrEmpty(PathTo7Zip))
                throw new Exception("Path to 7Zip executable not specified.");

            builder.RegisterType<SevenZipWrapper>()
                .WithParameter("pathTo7Zip", PathTo7Zip)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

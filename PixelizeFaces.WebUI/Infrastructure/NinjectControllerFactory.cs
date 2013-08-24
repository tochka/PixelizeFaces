using System;
using FaceDetection;
using Ninject;
using System.Web.Mvc;
using Ninject.Parameters;
using PixelizeFaces.Domain.Abstract;
using PixelizeFaces.Domain.Concrete;

namespace PixelizeFaces.WebUI.Infrastructure

{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory(string serverPath)
        {
            _ninjectKernel = new StandardKernel();
            AddBindings(serverPath);
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings(string serverPath)
        {
            _ninjectKernel.Bind<IPictureRepository>().To<EFPictureRepository>();

            // TODO Использовать относительный путь            
            var faceDetect = new DetectFace(serverPath+"bin\\haarcascade_frontalface_default.xml");
            _ninjectKernel.Bind<DetectFace>().ToConstant(faceDetect);
            _ninjectKernel.Bind<IFiltering>().To<Pixelate>()
                .WithConstructorArgument("pixelsX",7)
                .WithConstructorArgument("pixelsY",7);                
        }
    }
}
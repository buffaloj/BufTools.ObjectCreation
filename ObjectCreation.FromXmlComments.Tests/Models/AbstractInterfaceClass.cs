namespace ObjectCreation.FromXmlComments.Tests.Models
{
    public class AbstractInterfaceClass : AbstractBaseClass, ITestInterface
    {
        public override void AbstractMethod()
        {
            throw new NotImplementedException();
        }

        public int InterfaceMethod()
        {
            throw new NotImplementedException();
        }
    }
}

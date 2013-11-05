namespace ProductinWithInheritance
{
    interface ICustomLogger
    {
        void Write(LogMessage msg);
        void SetTarget(string locationOfFileOrService);
        void Enable();
        void Disable();

    }
}
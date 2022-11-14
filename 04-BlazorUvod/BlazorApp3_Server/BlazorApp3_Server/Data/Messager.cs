namespace BlazorApp3_Server.Data
{
    public class Messager
    {
        private Messager() { }

        private static Messager current;
        public static Messager Current
            => current ?? (current = new Messager());

        public event EventHandler OnMessage;

        public void Changed()
        {
            OnMessage?.Invoke(this, EventArgs.Empty);
        }
    }
}

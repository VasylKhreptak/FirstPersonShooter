using UnityEngine;
using Zenject;

namespace Graphics
{
    public class CursorUnlocker : ITickable
    {
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.None;
        }
    }
}
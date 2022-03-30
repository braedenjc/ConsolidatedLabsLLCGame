namespace DebuggingTools{
    using UnityEngine;
    public class PathTools
    {
        //Draw a line of a color from the center of one Vector3 to another.
        public void DrawLineFromCenter(Vector3 from, Vector3 to, Color color){
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
        }
    }
}

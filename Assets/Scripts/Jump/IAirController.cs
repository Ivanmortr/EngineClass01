﻿namespace Jump
{
    public interface IAirController
    {
        public bool Jumping { get; set; }
        public bool IsFalling { get; set; }
        public bool IsGrounding { get; set; }
    }
}
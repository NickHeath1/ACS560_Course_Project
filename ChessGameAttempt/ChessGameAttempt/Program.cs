﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    static class Program
    {
        // Settings
        //public PieceImageSettings;
        //public SquareColorSettings = new SquareColorSettings();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LobbyForm(new User("mouthymouth", "")));
            //Application.Run(new LogInForm());
            Application.Run(new LogInForm());
        }
    }
}

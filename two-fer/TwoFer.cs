﻿using System;

  public static class TwoFer
  {
      public static string Name()
      {
          return Name("you");
      }

      public static string Name(string input)
      {
          return $"One for {input}, one for me.";
      }
  }

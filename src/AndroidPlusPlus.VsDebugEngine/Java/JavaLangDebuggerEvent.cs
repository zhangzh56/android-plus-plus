﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Debugger.Interop;

using AndroidPlusPlus.Common;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace AndroidPlusPlus.VsDebugEngine
{

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public class JavaLangDebuggerEvent
  {

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [ComVisible (true)]
    [ClassInterface (ClassInterfaceType.None)]
    [Guid ("11751C50-464E-4B01-B5A7-0D9122BCEC38")]
    public sealed class AttachClient : AsynchronousDebugEvent, IDebugEvent2
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [ComVisible (true)]
    [ClassInterface (ClassInterfaceType.None)]
    [Guid ("9BD68CAB-30A0-4354-8EE6-27702414E88C")]
    public sealed class DetachClient : AsynchronousDebugEvent, IDebugEvent2
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [ComVisible (true)]
    [ClassInterface (ClassInterfaceType.None)]
    [Guid ("8EDD7F87-C917-4E2F-A246-2D73CEA495CC")]
    public sealed class StopClient : AsynchronousDebugEvent, IDebugEvent2
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [ComVisible (true)]
    [ClassInterface (ClassInterfaceType.None)]
    [Guid ("66DF49B1-683A-4DA9-AC14-E9716C8F2F73")]
    public sealed class ContinueClient : AsynchronousDebugEvent, IDebugEvent2
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [ComVisible (true)]
    [ClassInterface (ClassInterfaceType.None)]
    [Guid ("6FB40A90-377A-4039-B42B-74B66A69EA97")]
    public sealed class TerminateClient : AsynchronousDebugEvent, IDebugEvent2
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  }

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

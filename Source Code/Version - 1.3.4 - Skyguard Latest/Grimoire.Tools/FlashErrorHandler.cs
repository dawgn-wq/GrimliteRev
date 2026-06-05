using System;
using AxShockwaveFlashObjects;

namespace Grimoire.Tools;

public delegate void FlashErrorHandler(AxShockwaveFlash flash, Exception e, string function, params object[] args);

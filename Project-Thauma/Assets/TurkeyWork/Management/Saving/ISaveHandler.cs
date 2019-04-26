using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Management {

    public interface ISaveHandler {

        string SaveFileName { get; }

        void OnSaveData ();

        void OnLoadData ();

    }

}
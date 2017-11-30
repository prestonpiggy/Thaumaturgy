using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Management {

    public interface ISaveHandler {

        void OnSaveData ();

        void OnLoadData ();

    }

}
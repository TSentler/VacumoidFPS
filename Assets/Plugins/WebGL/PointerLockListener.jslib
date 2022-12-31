mergeInto(LibraryManager.library, {
    PointerLockListenerInitialize: function () {
        if (typeof pointerLockListenerInitialize === 'undefined'){
            var script = document.createElement("script");
            script.textContent = `
                var pointerLockListenerInitialize=1;
                function onLockCursorChanged(e) {
                    let isLock = +(document.pointerLockElement != null ||
                            document.mozPointerLockElement != null ||
                            document.webkitPointerLockElement != null);
                    console.log('Lock status = ' + (isLock));
                    myGameInstance.SendMessage("JavascriptHook", "OnLockCursorChanged", isLock);
                }
            `;
            document.head.appendChild(script);
            console.log('pointerLockListenerInitialize');
        }else{
            console.log('pointerLockListenerInitialize = ' + pointerLockListenerInitialize);
        }       
    },
    
    AddPointerLockListener: function () {
        document.addEventListener('pointerlockchange', onLockCursorChanged, false);
        document.addEventListener('mozpointerlockchange', onLockCursorChanged, false);
        document.addEventListener('webkitpointerlockchange', onLockCursorChanged, false);
    },
    
    RemovePointerLockListener: function () {
        document.removeEventListener('pointerlockchange', onLockCursorChanged, false);
        document.removeEventListener('mozpointerlockchange', onLockCursorChanged, false);
        document.removeEventListener('webkitpointerlockchange', onLockCursorChanged, false);
    }
    
});
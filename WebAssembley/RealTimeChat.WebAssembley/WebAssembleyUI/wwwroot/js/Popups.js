export class Popup {

    constructor(message,type) {
        this.Message = message;
        this.Type = type;

        //Constans
        this.Options = { "error": "popup-standard-error" }
        this.OverlayClass = "overlay"
        this.CloseButtonClass = "close-button"
    }

    RegisterCloseEvent = () => {
        document.addEventListener('click', this.ClosePopup)
    }

    ClosePopup = () => {
        const body = document.querySelector('body')
        if (this.Popup) {
            body.removeChild(this.Overlay)
        }
    }

    ShowPopup = () => {
        const body = document.querySelector('body')
        this.Popup = document.createElement('div')
        this.Overlay = document.createElement('div')
        this.CloseButton = document.createElement('button')
        this.PopupMessage = document.createElement('div')
        
        if (this.Options[this.Type]) {
            this.CloseButton.innerHTML = 'X'
            this.PopupMessage.innerHTML = this.Message

            //Add necessary classes
            this.CloseButton.classList.add(this.CloseButtonClass)
            this.Popup.classList.add(this.Options[this.Type])
            this.Overlay.classList.add(this.OverlayClass)
            this.PopupMessage.classList.add('popup-message')

            //Append created elements
            this.Popup.appendChild(this.PopupMessage)
            this.Popup.appendChild(this.CloseButton)
            this.Overlay.appendChild(this.Popup)
            body.appendChild(this.Overlay)
        }
    }

    
}
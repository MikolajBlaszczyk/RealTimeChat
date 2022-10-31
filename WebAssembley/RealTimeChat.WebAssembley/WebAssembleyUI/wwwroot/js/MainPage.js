const FullNameRTC = "Images/FullNameRTC.png";
const LogoRTC = "Images/LogoRTC.png";
//Classes used to swap images of Logo
const LogoOne = "logo-swap-one";
const LogoTwo = "logo-swap-two";
const LogoThree = "logo-swap-three";
//
//Classes used to open chat window
const openChatWindowClass = "chat-window-open"
const closeChatWindowClass = "chat-window-close"
//
const navUsers = document.getElementById('nav-users');
const Users = document.getElementById('users');
const TimeoutFullNameRTC = 500 + 100;


navUsers.addEventListener('click', (eventTarget) => {
    ToggleLogo();
    ExtendUsersList();
})


//TODO: Fix resizing at first phase "LogoOne"
const ToggleLogo = () => {
    const img = document.getElementById('nav-logo')
    var dir = GetDir(FullNameRTC)
    if (img.src !== dir) {
        SwapImages(true, img)
    }
    else {
        SwapImages(false, img)
    }
}

//STUPID FUCKING LOGIC ALERT 
// I dont have idea how to handle it better right now, however it should work TODO: ref this to be better especially naming 
// true - Swap Logo to FullName
// false - Swap FullName to Logo
const SwapImages = (SwapLogoToFullName, image) => {
    if (SwapLogoToFullName) {
        SwapOneToTwo(image)
    }
    else {
        SwapTwoToThree(image)
    }
}
const SwapOneToTwo = (image) => {
    if (image.classList.contains(LogoThree) == true) {
        image.classList.remove(LogoThree)
    }

    image.classList.add(LogoOne)
    navUsers.style.backgroundPosition = 'left'

    setTimeout(() => {
        image.src = FullNameRTC
        image.classList.remove(LogoOne)
        image.classList.add(LogoTwo)
    }, TimeoutFullNameRTC)
}
const SwapTwoToThree = (image) => {
    image.src = LogoRTC
    image.classList.remove(LogoTwo)
    image.classList.add(LogoThree)
    navUsers.style.backgroundPosition = 'right'
}
//STUPID FUCKING LOGIC ALERT

const ExtendUsersList = () => {
    Users.classList.toggle('users-extend')
}

const GetDir = (path) => {
    return `${document.location.protocol}//${document.location.host}/${path}`;
}

export function ToggleChatWindow() {
    ToggleChat()
}


const ToggleChat = () => {
    const chatWindow = document.getElementById('chat-window')

    if (chatWindow.classList.contains(openChatWindowClass) === false) {
        chatWindow.classList.remove(closeChatWindowClass)
        chatWindow.classList.add(openChatWindowClass)
    }
    else {
        chatWindow.classList.remove(openChatWindowClass)
        chatWindow.classList.add(closeChatWindowClass)
    }
}

export function ToggleSettingsWindow() {
    const settingsWindow = document.getElementById('')
}
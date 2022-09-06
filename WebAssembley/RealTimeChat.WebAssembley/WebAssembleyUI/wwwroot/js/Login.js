export const LoginModule = (input) => {
    let usernameInput = document.getElementById("username-input");
    if (input === true) {
        let span = document.createElement('span')
        span.classList.add('username-text')
        span.innerText = 'Username'
        console.log(span)
        usernameInput.parentElement.appendChild(span)
    }
    else {
        let span = document.querySelector('.username-text')

        if (span) {
            span.remove()
        }
    }
}


window.showLiveToastError = () => {
    const toastEl = document.getElementById('liveToastError');
    if (toastEl) {
        const toast = bootstrap.Toast.getOrCreateInstance(toastEl);
        toast.show();
    }
};

window.triggerClick = (element) => element.click();

setTimeout(function () {
    const quill = new Quill('#editor', {
        modules: {
            toolbar: [
                // [{ header: [1, 2, false] }],
                // ['bold', 'italic', 'underline'],
                ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
                ['blockquote', 'code-block', 'link', 'image'],

                [{ 'header': 1 }, { 'header': 2 }],               // custom button values
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
                [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
                [{ 'direction': 'rtl' }],                         // text direction

                [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
                [{ 'font': [] }],
                [{ 'align': [] }],

                ['clean']
            ],
        },
        placeholder: 'Compose a product description here...',
        theme: 'snow'
    });
    window.quillEditor = quill;
    console.log("✅ Quill initialized");
}, 1000); // Delay in milliseconds (1000ms = 1 second)

window.setContent = function (deltaJson) {
    const Delta = Quill.import('delta');
    const delta = typeof deltaJson === 'string' ? JSON.parse(deltaJson) : deltaJson;
    if (window.quillEditor) {
        const deltaObj = new Delta(delta);
        window.quillEditor.setContents(deltaObj, 'api')
    } else {
        console.warn("Quill editor not initialized.");
    }
};


// Quill.on(Quill.events.TEXT_CHANGE, update);
// const playground = document.querySelector('#playground');
// update();

// function formatDelta(delta) {
//     return `<div>${JSON.stringify(delta.ops, null, 2)}</div>`;
// }

// function update(delta) {
//     const contents = quill.getContents();
//     let html = `<h3>contents</h3>${formatDelta(contents)}`
//     if (delta) {
//         html = `${html}<h3>change</h3>${formatDelta(delta)}`;
//     }
//     playground.innerHTML = html;
// }

window.triggerValidation = (items) = () => { // discontinued

}

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl)) // I'm having an issue rendering the Bootstrap tooltips in my Razor page.
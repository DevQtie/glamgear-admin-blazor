// function onBeforeUnload(e) {
//     if (thereAreUnsavedChanges()) {
//         e.preventDefault();
//         e.returnValue = '';
//         return;
//     }

//     delete e['returnValue'];
// }

// window.addEventListener('beforeunload', onBeforeUnload);

// window.onbeforeunload = function (e) {
//     e = e || window.event;

//     // For IE and Firefox prior to version 4
//     if (e) {
//         e.returnValue = 'Sure?';
//     }

//     // For Safari
//     return 'Sure?';
// };

// window.addEventListener('beforeunload', function (e) {
//     if (thereAreUnsavedChanges()) {
//         e.preventDefault();           // Required for Chrome and modern browsers
//         e.returnValue = '';           // Triggers the confirmation dialog
//     }
// });

window.onbeforeunload = function (e) { // I'm inspired by the Stack Overflow behavior where it asks for confirmation only if there are unsaved changes when you're composing a question or answer.
    return 'Dialog text here.'; // need for improvement to avoid (if necessary) the entries of unsaved changes.
};

window.goBack = function () {
    history.back();
};

window.showLiveToastError = () => {
    const toastEl = document.getElementById('liveToastError');
    if (toastEl) {
        const toast = bootstrap.Toast.getOrCreateInstance(toastEl);
        toast.show();
    }
};

window.triggerClick = (element) => element.click();

const Font = Quill.import('attributors/style/font');
Font.whitelist = [
    'roboto', 'open sans', 'lato', 'montserrat', 'roboto condensed',
    'oswald', 'poppins', 'slabo 27px', 'noto sans', 'roboto mono', 'merriweather'
];
Quill.register(Font, true);

window.previewContent = function () {
    const quill = new Quill('#editor', {
        modules: {
            toolbar: '#toolbar-container'
        },
        placeholder: 'Compose a product description here...',
        theme: 'snow'
    });
    window.quillEditor = quill;
    console.log("Quill initialized");
};

window.setContent = function (deltaJson) {
    const Delta = Quill.import('delta');
    const delta = typeof deltaJson === 'string' ? JSON.parse(deltaJson) : deltaJson;
    if (window.quillEditor) {
        const deltaObj = new Delta(delta);
        window.quillEditor.setContents(deltaObj, 'api');
    } else {
        console.warn("Quill editor not initialized.");
    }
};

window.getContentsQuill = function () {
    if (window.quillEditor) {
        const delta = window.quillEditor.getContents();
        return JSON.stringify(delta);
    } else {
        console.warn("Quill editor not initialized.");
        return null;
    }
};

window.previewContentReview = function () {
    const quill = new Quill('#editorReview', {
        readOnly: true,
        modules: {
            toolbar: false,
        },
        theme: 'bubble' // change to snow to see border, otherwise use bubble
    });
    window.quillEditorReview = quill;
    console.log("Quill initialized");
};

window.setContentReview = function (deltaJson) {
    const Delta = Quill.import('delta');
    const delta = typeof deltaJson === 'string' ? JSON.parse(deltaJson) : deltaJson;
    if (window.quillEditorReview) {
        const deltaObj = new Delta(delta);
        window.quillEditorReview.setContents(deltaObj, 'api');
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
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

window.onbeforeunload = function (e) {
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
    setTimeout(function () {
        const quill = new Quill('#editor', {
            modules: {
                toolbar: [ // I have no idea how to specify this id: #toolbar-container
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
                    [{ 'font': Font.whitelist }],
                    [{ 'align': [] }],

                    ['clean']
                ],
            },
            placeholder: 'Compose a product description here...',
            theme: 'snow'
        });
        window.quillEditor = quill;
        console.log("Quill initialized");
    }, 1000);
};

window.setContent = function (deltaJson) {
    setTimeout(function () {
        const Delta = Quill.import('delta');
        const delta = typeof deltaJson === 'string' ? JSON.parse(deltaJson) : deltaJson;
        if (window.quillEditor) {
            const deltaObj = new Delta(delta);
            window.quillEditor.setContents(deltaObj, 'api');
        } else {
            console.warn("Quill editor not initialized.");
        }
    }, 1000); // Delay in milliseconds (1000ms = 1 second)
};

window.previewContentReview = function () {
    setTimeout(function () {
        const quill = new Quill('#editorReview', {
            readOnly: true,
            modules: {
                toolbar: false,
            },
            placeholder: 'Compose a product description here...',
            theme: 'snow'
        });
        window.quillEditorReview = quill;
        console.log("Quill initialized");
    }, 1000);
};

window.setContentReview = function (deltaJson) {
    setTimeout(function () {
        const Delta = Quill.import('delta');
        const delta = typeof deltaJson === 'string' ? JSON.parse(deltaJson) : deltaJson;
        if (window.quillEditorReview) {
            const deltaObj = new Delta(delta);
            window.quillEditorReview.setContents(deltaObj, 'api');
        } else {
            console.warn("Quill editor not initialized.");
        }
    }, 1000); // Delay in milliseconds (1000ms = 1 second)
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
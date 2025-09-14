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
}, 1000); // Delay in milliseconds (1000ms = 1 second)

window.showLiveToastError = () => {
    const toastEl = document.getElementById('liveToastError');
    if (toastEl) {
        const toast = bootstrap.Toast.getOrCreateInstance(toastEl);
        toast.show();
    }
};

window.triggerClick = (element) => element.click();

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl)) // I'm having an issue rendering the Bootstrap tooltips in my Razor page.
setTimeout(function () {
    const quill = new Quill('#editor', {
        modules: {
            toolbar: [
                [{ header: [1, 2, false] }],
                ['bold', 'italic', 'underline'],
                ['image', 'code-block'],
            ],
        },
        placeholder: 'Compose a product description here...',
        theme: 'snow'
    });
}, 1000); // Delay in milliseconds (1000ms = 1 second)
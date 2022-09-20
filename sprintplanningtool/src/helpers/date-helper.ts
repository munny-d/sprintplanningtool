export const dateHelper = {
    formatDate,
};

function formatDate(date: Date | string) {
    date = new Date(date);
    return date.toLocaleDateString('en-GB');
}

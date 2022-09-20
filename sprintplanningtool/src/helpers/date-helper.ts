export const dateHelper = {
    formatDate,
    addWeeks,
};

function formatDate(date: Date) {
    console.log('DATE IN HELPER: ', date);

    const newDate = [
        padToTwoDigits(date.getDate()),
        padToTwoDigits(date.getMonth() + 1),
        date.getFullYear(),
    ].join('-');

    console.log('NEW DATE: ', newDate);
}

function padToTwoDigits(num: number) {
    num.toString().padStart(2, '0');
}

function addWeeks(numOfWeeks: number, date = new Date()) {
    const dateCopy = new Date(date.getTime());
    dateCopy.setDate(dateCopy.getDate() + numOfWeeks * 7);
    return dateCopy;
}

// importing goal model
import MacroLog from "../models/macroLog.js"

// create method - creates a new macro goal or updates if there's one for the current day
export const createMacroLog = async (req, res) => {
    // sets the user variable to the id of the user making the request
    req.body.user = req.user.id;

    // sets startDate to current date & time
    req.body.date = new Date();

    // logs the food
    await new MacroLog(req.body).save()
        .then((logLog) => {
            // returns the logged food
            res.status(201).json({ msg: 'food logged', logLog })
        })
        .catch((error) => {
            // logs and returns status 500 if error -> food log not created
            console.log(error)
            res.status(500).json({ msg: 'unable to log food' })
        })
}

// get method - gets all food logs for day or multiple days
export const getMacroLogDayOrDays = async (req, res) => {

    // sets search start and end date depending on if theres query params present
    let searchDate = null;
    let searchDateEnd = null;
    if (Object.keys(req.query).length > 0) {
        searchDate = new Date(req.query.startDate);
        searchDateEnd = new Date(req.query.endDate);
        searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);
    } else {
        searchDate = new Date();
        searchDate.setUTCHours(0, 0, 0, 0);
        searchDateEnd = new Date(searchDate);
        searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);
    }

    // finds macro log between two dates and the has user
    MacroLog.find({
        user: req.user.id,
        date: {
            $gte: searchDate,
            $lt: searchDateEnd
        }
    }).populate('food')
        .then((MacroLogItems) => {
            res.status(200).json({ MacroLogItems });
        })
        .catch((error) => {
            // logs and returns status 500 error if failed or no goals found
            console.log(error)
            res.status(500).json({ msg: "unable to find any goals" })
        })
};
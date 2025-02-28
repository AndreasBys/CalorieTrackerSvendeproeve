// importing goal model
import MacroTrack from "../models/macroTrack.js"

// create method - creates a new macro goal or updates if there's one for the current day
export const createMacroTrack = async (req, res) => {
    // sets the user variable to the id of the user making the request
    req.body.user = req.user.id;

    // sets startDate to current date & time
    req.body.date = new Date();

    // logs the food
    await new MacroTrack(req.body).save()
    .then((trackLog) => {
        // returns the logged food
        res.status(201).json({ msg: 'food logged', trackLog })
    })
    .catch((error) => {
        // logs and returns status 500 if error -> food log not created
        console.log(error)
        res.status(500).json({ msg: 'unable to log food' })
    })
}

// get method - gets all food logs for day
export const getMacroTrackDay = async (req, res) => {
    // sets search Date variables from current date and next day at 00:00
    const searchDate = new Date(req.query.date);
    const searchDateEnd = new Date(searchDate)
    searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);

    // finds goals between two dates and the has user
    MacroTrack.find({
        user: req.user.id,
        date: {
            $gte: searchDate,
            $lt: searchDateEnd
        }
    })
    .then((goals) => {
        res.status(200).json({ goals: goals});
    })
    .catch((error) => {
        // logs and returns status 500 error if failed or no goals found
        console.log(error)
        res.status(500).json({ msg: "unable to find any goals" })
    })
};
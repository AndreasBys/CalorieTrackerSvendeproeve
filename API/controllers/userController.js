// importing user model
import User from "../models/user.js"

// exporting get all method
export const getUsers = async (req, res) => {
    // if the user isn't admin error returns
    if (!req.user.admin)
        return res.status(401).json({ message: 'No access' });

    await User.find()
        .then((users) => {
            // returns status 200 and all users
            res.status(200).json({ users: users })
        })
        .catch((error) => {
            // logs and return status 500 error if failed
            console.log(error)
            res.status(500).json({ msg: "unable to get users" })
        })

}

// exporting get method - gets specific user through Id
export const getUser = async (req, res) => {
    // gets id from request params
    const id = req.params.id

    // if the user isn't admin or userId doesn't match the user that requests
    // error returns
    if (!(req.user.admin || req.user.id == id))
        return res.status(401).json({ message: 'No access' });

    // finds user through id
    // populate('goals') to reference this method in goalsController 
    User.findById(id).populate('goals')
        .then((users) => {
            // returns status 200 and the user
            res.status(200).json({ user: users })
        })
        .catch((error) => {
            // logs and returns status 500 error if failed or no users found
            console.log(error)
            res.status(500).json({ msg: "unable to get user" })
        })
};

// exporting search method - gets users that match certain criterias
export const search = async (req, res) => {
    // if the user isn't admin error returns
    if (!req.user.admin)
        return res.status(401).json({ message: 'No access' });

    // creates search term from the request query
    const searchTerm = req.query.searchTerm

    // creates regex for search
    const searchRegex = new RegExp(searchTerm, "i")

    // specifies criterias to search for
    await User.find({
        $or: [
            { usersame: searchRegex },
            { email: searchRegex },
            { password: searchRegex },
            { alder: searchRegex },
            { vægt: searchRegex },
            { højde: searchRegex },
        ]
    })
        .then((users) => {
            if (users.lenght) {
                // returns the users that match
                res.status(200).json({ users: users })
            }
            else {
                // returns nothing if no users match
                res.status(200).json({ users: [], msg: "no users found" })
            }
        })
        .catch((error) => {
            // logs and returns status 500 if error
            console.log(error)
            res.status(500).json({ msg: "unable to find user" })
        });
};

// exporting create method - creates new user
export const createUser = async (req, res) => {
    // if the user isn't admin error returns
    if (!req.user.admin)
        return res.status(401).json({ message: 'No access' });

    // saves the new user to database
    await new User(req.body).save()
        .then((user) => {
            // returns the created user
            res.status(201).json({ msg: 'user saved', user })
        })
        .catch((error) => {
            // logs and returns status 500 if error -> goal not created
            console.log(error)
            res.status(500).json({ msg: 'unable to create new user' })
        })
}

// exporting delete method - deletes a user through id
export const deleteUser = async (req, res) => {
    // gets user id from request params
    const id = req.params.id

    // if the user isn't admin or userId isn't matching error returns
    if (!(req.user.admin || req.user.id == id))
        return res.status(401).json({ message: 'No access' });

    // deletes the user from database
    await User.findByIdAndDelete(id)
        .then((user) => {
            if (!user) {
                throw new Error("user not found")
            }
            // returns the deleted user
            res.status(200).json({ msg: "Following user has been deleted", user: user })
        })
        .catch((error) => {
            // logs and returns status 500 if error 
            // user not deleted or couldn't be found
            console.log(error)
            res.status(500).json({ msg: "unable to get user" })
        })
}

// exporting update method - updates a user through id
export const updateUser = async (req, res) => {
    // gets user id from request params
    const id = req.params.id

    // if the user isn't admin or userId isn't matching error returns
    if (!(req.user.admin || req.user.id == id))
        return res.status(401).json({ message: 'No access' });

    // updates the goal in database with the updated user
    await User.findOneAndUpdate({ _id: id },
        req.body,
        { new: true, runValidators: true })
        .then((updatedUser) => {
            // returns the updated user
            res.status(200).json({ msg: "user updated", user: updatedUser })
        })
        .catch((error) => {
            // logs and returns status 500 if error 
            // goal couldn't be found or couldn't be updated
            console.log(error)
            res.status(500).json({ msg: "unable to update user" })
        });
}
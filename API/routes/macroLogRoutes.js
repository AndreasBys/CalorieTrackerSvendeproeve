// importing express module
import express from 'express'
import authenticate from '../middleware/authenticator.js';
import { createMacroLog, deleteMacroLog, getMacroLogDayOrDays, updateMacroLog } from '../controllers/macroLogController.js';

// creates new router from express module
const router = express.Router();

// defining routes
router.post('/', authenticate, createMacroLog);
router.get('/', authenticate, getMacroLogDayOrDays);
router.delete('/:id', authenticate, deleteMacroLog);
router.patch('/:id', authenticate, updateMacroLog);

// exports router as default
export default router;
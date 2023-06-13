using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Demo.Domain.Validators
{
	public class ApiGameValidator : AbstractValidator<ApiGame>
	{
		public ApiGameValidator()
		{
			//RuleFor(x => x.Id).NotEqual(-1);
			RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
			RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Release date is required");
			RuleFor(x => x.Team).NotEmpty().WithMessage("Team is required");
			RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating is required");
			RuleFor(x => x.Rating).GreaterThan(-0.00001d).WithMessage("Rating is required");
			RuleFor(x => x.Genres).NotEmpty().WithMessage("Genres is required");

		}
	}
}
